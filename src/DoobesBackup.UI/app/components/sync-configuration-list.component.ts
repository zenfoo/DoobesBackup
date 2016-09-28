import { Component, OnInit } from "@angular/core";
import { Hero } from "../models/hero.model";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { SyncConfiguration } from "../models/sync-configuration.model";

@Component({
    selector: "main-panel",
    template: `
<h2>{{title}}</h2>
<ul class="sync-configurations">
    <li 
        *ngFor="let syncConfig of syncConfigurations" 
        (click)="onSelect(syncConfig)"
        [class.selected]="syncConfig === selectedSyncConfig">
        <span class="badge">{{syncConfig.id}}</span> {{syncConfig.name}}
    </li>
</ul>
`,
    styles: [`
  .selected {
    background-color: #CFD8DC !important;
    color: white;
  }
  .heroes {
    margin: 0 0 2em 0;
    list-style-type: none;
    padding: 0;
    width: 15em;
  }
  .heroes li {
    cursor: pointer;
    position: relative;
    left: 0;
    background-color: #EEE;
    margin: .5em;
    padding: .3em 0;
    height: 1.6em;
    border-radius: 4px;
  }
  .heroes li.selected:hover {
    background-color: #BBD8DC !important;
    color: white;
  }
  .heroes li:hover {
    color: #607D8B;
    background-color: #DDD;
    left: .1em;
  }
  .heroes .text {
    position: relative;
    top: -3px;
  }
  .heroes .badge {
    display: inline-block;
    font-size: small;
    color: white;
    padding: 0.8em 0.7em 0 0.7em;
    background-color: #607D8B;
    line-height: 1em;
    position: relative;
    left: -1px;
    top: -4px;
    height: 1.8em;
    margin-right: .8em;
    border-radius: 4px 0 0 4px;
  }
`]
})

export class SyncConfigurationListComponent implements OnInit {
    title:string = "Sync configurations";
    selectedSyncConfiguration: SyncConfiguration;
    syncConfigurations: SyncConfiguration[];

    constructor(private syncConfigurationService: SyncConfigurationService) {
        // this.heroes = this.syncConfigurationService.getAll();
        // alert(syncConfigurations.count);
    }
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

}

