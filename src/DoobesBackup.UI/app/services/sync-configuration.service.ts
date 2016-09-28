import { Injectable } from "@angular/core";
import { HEROES } from "./mock-heroes";
import { Hero } from "../models/hero.model";
import { SyncConfiguration } from "../models/sync-configuration.model";
import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { AppConfiguration } from "../app-configuration";
import "rxjs/add/operator/map";
import "rxjs/Rx";

@Injectable()
export class SyncConfigurationService {
    
    constructor(private _http: Http, private _configuration: AppConfiguration) { }
    
    getAllConfigurations(): Promise<SyncConfiguration[]> {
        return this._http.get(this._configuration.ServerWithBaseUrl + "syncconfigurations")
            .toPromise()
            .then((response: Response) => {
                var data = <SyncConfiguration[]>response.json();
                return data;
            })
            .catch(this.handleError);
    }

    getById(id: string): Promise<SyncConfiguration> {
        return this._http.get(this._configuration.ServerWithBaseUrl + "syncconfigurations/" + id)
            .toPromise()
            .then((response: Response) => {
                var data = <SyncConfiguration>response.json();
                return data;
            })
            .catch(this.handleError);
    }
    
    private handleError(error: Response): {} {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    };
}