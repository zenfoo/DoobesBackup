﻿import { Component, OnInit } from "@angular/core";
import { HealthCheckService } from "../services/health-check.service";
import { Observable } from "rxjs/Rx";

@Component({
    moduleId: module.id,
    selector: "doobes-app",
    templateUrl: "app.component.html"
})

export class AppComponent implements OnInit {
    title: string = "Doobes Backup";
    isBackendConnectionHealthy: boolean = false;

    constructor(private healthCheckService: HealthCheckService) { }

    ngOnInit(): void {
        this.checkHealth();
    }
    
    checkHealth(): void {
        this.healthCheckService.isHealthy()
            .then((isHealthy: boolean) => {
                if (!isHealthy) {
                    alert("Could not connect to backend service! Click OK to reload the app.");
                    window.location.replace("/");
                } else {
                    this.isBackendConnectionHealthy = true;
                    this.initializeHealthCheckPolling();
                }
            });
    }

    initializeHealthCheckPolling(): void {
        Observable
            .interval(10000) // Poll every 10 secs
            .subscribe((x: number) => {
                console && console.log("[Healthcheck] Testing...");
                this.healthCheckService.isHealthy()
                    .then((isHealthy: boolean) => {
                        if (!isHealthy && this.isBackendConnectionHealthy) {
                            this.isBackendConnectionHealthy = false;
                            console && console.log(`[Healthcheck] Lost connection with backend!`);
                            alert("Lost connection with backend service! Click OK to reload the app.");
                            window.location.replace("/");
                            return;
                        } else if (!isHealthy) {
                            console && console.log("[Healthcheck] Still unhealthy...");
                        } else {
                            console && console.log("[Healthcheck] OK");
                        }
                    });
            });
    }
}