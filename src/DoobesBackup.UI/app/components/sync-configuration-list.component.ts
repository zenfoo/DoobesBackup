import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { SyncConfiguration } from "../models/sync-configuration.model";

@Component({
    moduleId: module.id,
    selector: "sync-configuration-list-panel",
    templateUrl: "sync-configuration-list.component.html"
})

export class SyncConfigurationListComponent implements OnInit {
    title:string = "Sync configurations";
    selectedSyncConfiguration: SyncConfiguration;
    syncConfigurations: SyncConfiguration[];

    constructor(
        private syncConfigurationService: SyncConfigurationService,
        private router: Router) { }

    onSelect(syncConfig: SyncConfiguration): void {
        this.selectedSyncConfiguration = syncConfig;
    }

    getSyncConfigurations():void {
        this.syncConfigurationService.getAllConfigurations()
            .then((syncConfigurations: SyncConfiguration[]) => {
                this.syncConfigurations = syncConfigurations
            });
    }

    ngOnInit():void {
        this.getSyncConfigurations();
    }

    onAddBtnClick(): void {
        this.router.navigate(["/syncconfigurations", "add"]);
    }

}

