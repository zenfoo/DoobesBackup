import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { AppConfiguration } from "../app-configuration";
import "rxjs/add/operator/map";
import "rxjs/Rx";

@Injectable()
export class HealthCheckService {
    
    constructor(private _http: Http, private _configuration: AppConfiguration) { }

    isHealthy(): Promise<boolean> {
        return this._http.get(this._configuration.ServerWithBaseUrl + "healthcheck")
            .toPromise()
            .then((response: Response) => {
                return true;
            })
            .catch((error: Response) => {
                return false;
            });
    }
}