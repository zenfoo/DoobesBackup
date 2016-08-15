import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent }  from "../components/app.component";
import { HeroesComponent } from "../components/heroes.component";
import { HeroDetailComponent } from "../components/hero-detail.component";
import { AppConfiguration } from "../app-configuration";
import { SyncConfigurationService } from "../services/sync-configuration.service";

@NgModule({
    imports: [ BrowserModule, FormsModule ],
    declarations: [ AppComponent, HeroesComponent, HeroDetailComponent],
    providers: [ AppConfiguration, SyncConfigurationService ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }