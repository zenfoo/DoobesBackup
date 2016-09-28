﻿import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent }  from "../components/app.component";
import { HeroDetailComponent } from "../components/hero-detail.component";
import { SyncConfigurationListComponent } from "../components/sync-configuration-list.component";
import { AppConfiguration } from "../app-configuration";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { MainMenuComponent } from "../components/main-menu.component";
import { DashboardComponent } from "../components/dashboard.component";
import { routing } from "../app.routing";
import { NotFoundComponent } from "../components/not-found.component";

@NgModule({
    imports: [ BrowserModule, FormsModule, HttpModule, routing ],
    declarations: [ AppComponent, DashboardComponent, MainMenuComponent, SyncConfigurationListComponent, NotFoundComponent ],
    providers: [ AppConfiguration, SyncConfigurationService ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }