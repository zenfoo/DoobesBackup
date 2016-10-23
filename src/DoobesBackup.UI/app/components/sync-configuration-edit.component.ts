import { Component, OnInit } from "@angular/core";
import { Hero } from "../models/hero.model";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { SyncConfiguration } from "../models/sync-configuration.model";
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormGroup } from "@angular/forms";

@Component({
    moduleId: module.id,
    selector: "sync-configuration-edit-panel",
    templateUrl: "sync-configuration-edit.component.html"
})

export class SyncConfigurationEditComponent implements OnInit {
    title:string = "Edit sync configuration";
    model: SyncConfiguration = new SyncConfiguration();
    isEditMode: boolean = false;
    
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
                this.model = syncConfiguration;
            });
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            let id: string = params["id"]; // +params['id']; // (+) converts string 'id' to a number

            if (id.toLowerCase() == "add") {
                this.isEditMode = false;
            } else {
                this.isEditMode = true;
                this.loadSyncConfiguration(id);
            }
        });
    }

    onSubmit(syncConfigForm: FormGroup): void {
        if (this.isEditMode) {
            this.syncConfigurationService.update(this.model)
                .then(this.handleSubmitResult)
                .catch(this.handleSubmitError);
        } else {
            this.syncConfigurationService.create(this.model)
                .then(this.handleSubmitResult)
                .catch(this.handleSubmitError);
        }
    }

    onCancel(syncConfigForm: FormGroup): void {
        if (syncConfigForm.dirty && !confirm("Discard changes?")) {
            return;
        }

        this.router.navigate(["/syncconfigurations"]);
    }

    onDelete(): void {
        if (confirm("Are you sure you want to delete this configuration?")) {
            this.syncConfigurationService.deleteById(this.model.id)
                .then((success: boolean) => {
                    this.router.navigate(["/syncconfigurations"]);
                })
                .catch(this.handleSubmitError);
        }
    }
}

