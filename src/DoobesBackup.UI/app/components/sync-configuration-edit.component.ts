import { Component, OnInit } from "@angular/core";
import { Hero } from "../models/hero.model";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { SyncConfiguration } from "../models/sync-configuration.model";
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: "sync-configuration-edit-panel",
    template: `
<h2>{{title}}</h2>
<input type="text" value="{{syncConfig.name}}" name="name" />
<input type="button" name="submit" type="submit" value="Save" />
`
})

export class SyncConfigurationEditComponent implements OnInit {
    title:string = "Edit sync configuration";
    syncConfig: SyncConfiguration;

    constructor(
        private syncConfigurationService: SyncConfigurationService,
        private route: ActivatedRoute) { }
    
    loadSyncConfiguration(id: string):void {
        this.syncConfigurationService.getById(id)
            .then((syncConfiguration: SyncConfiguration) => {
                this.syncConfig = syncConfiguration
            });
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            let id: string = params["id"]; // +params['id']; // (+) converts string 'id' to a number
            this.loadSyncConfiguration(id);
        });
    }
}

