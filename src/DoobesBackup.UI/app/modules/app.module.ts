import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent }  from "../components/app.component";
import { HeroDetailComponent } from "../components/hero-detail.component";
import { SyncConfigurationListComponent } from "../components/sync-configuration-list.component";
import { SyncConfigurationEditComponent } from "../components/sync-configuration-edit.component";
import { AppConfiguration } from "../app-configuration";
import { SyncConfigurationService } from "../services/sync-configuration.service";
import { MainMenuComponent } from "../components/main-menu.component";
import { DashboardComponent } from "../components/dashboard.component";
import { routing } from "../app.routing";
import { NotFoundComponent } from "../components/not-found.component";
import { HealthCheckService } from "../services/health-check.service";

@NgModule({
    imports: [ BrowserModule, FormsModule, HttpModule, routing ],
    declarations: [
        AppComponent,
        DashboardComponent,
        MainMenuComponent,
        SyncConfigurationListComponent,
        SyncConfigurationEditComponent,
        NotFoundComponent],
    providers: [ AppConfiguration, SyncConfigurationService, HealthCheckService ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }