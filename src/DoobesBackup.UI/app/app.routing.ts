import { ModuleWithProviders }  from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SyncConfigurationListComponent }      from "./components/sync-configuration-list.component";
import { DashboardComponent } from "./components/dashboard.component";
import { NotFoundComponent } from "./components/not-found.component";

const appRoutes: Routes = [
    { path: "syncconfigurations", component: SyncConfigurationListComponent },
    { path: "", redirectTo: "/dashboard", pathMatch: "full" },
    { path: "dashboard", component: DashboardComponent },
    { path: '404', component: NotFoundComponent },
    { path: '**', redirectTo: "/404", pathMatch: "full" }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);