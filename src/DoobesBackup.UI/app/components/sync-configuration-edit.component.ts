import { Component, OnInit } from "@angular/core";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { SyncConfiguration } from "../models/sync-configuration.model";
import { DropDownOptionModel } from "../models/drop-down-option.model";
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormGroup } from "@angular/forms";
import { SyncConfigurationEditViewModel } from "../viewmodels/sync-configuration-edit.viewmodel";
import { BackupSourceConfigComponent } from "./backup-source-config.component";

@Component({
    moduleId: module.id,
    selector: "sync-configuration-edit-panel",
    templateUrl: "sync-configuration-edit.component.html"
})

export class SyncConfigurationEditComponent implements OnInit {
    title:string = "Edit sync configuration";
    model: SyncConfigurationEditViewModel = new SyncConfigurationEditViewModel();
    
    handleSubmitResult: (s:boolean) => void = (success: boolean): void => {
        if (success) {
            this.router.navigate(["/syncconfigurations"]);
        } else {
            alert("Put in an error handling message here");
        }
    }

    handleSubmitError: (e:any) => void = (error: any): void => {
        alert("Some error occurred");
    }

    constructor(
        private syncConfigurationService: SyncConfigurationService,
        private route: ActivatedRoute,
        private router: Router) { }
    
    loadSyncConfiguration(id: string):void {
        this.syncConfigurationService.getById(id)
            .then((syncConfiguration: SyncConfiguration) => {
                this.model.syncConfig = syncConfiguration;
            });
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            let id: string = params["id"]; // +params['id']; // (+) converts string 'id' to a number

            if (id.toLowerCase() == "add") {
                this.model.isEditMode = false;
            } else {
                this.model.isEditMode = true;
                this.loadSyncConfiguration(id);
            }
        });
    }

    onSubmit(syncConfigForm: FormGroup): void {
        if (this.model.isEditMode) {
            this.syncConfigurationService.update(this.model.syncConfig)
                .then(this.handleSubmitResult)
                .catch(this.handleSubmitError);
        } else {
            this.syncConfigurationService.create(this.model.syncConfig)
                .then(this.handleSubmitResult)
                .catch(this.handleSubmitError);
        }
    }

    onBackupSourceTypeChange(selectedValue: string): void {
        this.model.backupSourceType = parseInt(selectedValue);
    }

    onCancel(syncConfigForm: FormGroup): void {
        if (syncConfigForm.dirty && !confirm("Discard changes?")) {
            return;
        }

        this.router.navigate(["/syncconfigurations"]);
    }

    onDelete(): void {
        if (confirm("Are you sure you want to delete this configuration?")) {
            this.syncConfigurationService.deleteById(this.model.syncConfig.id)
                .then((success: boolean) => {
                    this.router.navigate(["/syncconfigurations"]);
                })
                .catch(this.handleSubmitError);
        }
    }
}

