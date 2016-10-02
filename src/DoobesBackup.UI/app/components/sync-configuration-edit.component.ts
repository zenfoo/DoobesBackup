import { Component, OnInit } from "@angular/core";
import { Hero } from "../models/hero.model";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { SyncConfiguration } from "../models/sync-configuration.model";
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    moduleId: module.id,
    selector: "sync-configuration-edit-panel",
    templateUrl: "sync-configuration-edit.component.html"
})

export class SyncConfigurationEditComponent implements OnInit {
    title:string = "Edit sync configuration";
    model: SyncConfiguration = new SyncConfiguration();

    constructor(
        private syncConfigurationService: SyncConfigurationService,
        private route: ActivatedRoute) { }
    
    loadSyncConfiguration(id: string):void {
        this.syncConfigurationService.getById(id)
            .then((syncConfiguration: SyncConfiguration) => {
                this.model = syncConfiguration;
            });
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            let id: string = params["id"]; // +params['id']; // (+) converts string 'id' to a number
            this.loadSyncConfiguration(id);
        });
    }
}

