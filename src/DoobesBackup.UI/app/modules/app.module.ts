﻿import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent }  from "../components/app.component";
import { HeroDetailComponent } from "../components/hero-detail.component";

@NgModule({
    imports: [ BrowserModule, FormsModule ],
    declarations: [ AppComponent, HeroDetailComponent ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }