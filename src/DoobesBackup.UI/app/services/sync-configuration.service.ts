import { Injectable } from "@angular/core";
import { SyncConfiguration } from "../models/sync-configuration.model";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { AppConfiguration } from "../app-configuration";
import 'rxjs/add/operator/toPromise';
import "rxjs/add/operator/map";
import "rxjs/Rx";

@Injectable()
export class SyncConfigurationService {
    
    constructor(private _http: Http, private _configuration: AppConfiguration) { }
    
    getAllConfigurations(): Promise<SyncConfiguration[]> {
        let url: string = this._configuration.ServerWithBaseUrl + "syncconfigurations";
        return this._http.get(url)
            .toPromise()
            .then((response: Response) => {
                var data: SyncConfiguration[] = <SyncConfiguration[]>response.json();
                return data;
            })
            .catch(this.handleError);
    }

    getById(id: string): Promise<SyncConfiguration> {
        let url: string = `${this._configuration.ServerWithBaseUrl}syncconfigurations/${id}`;

        return this._http.get(url)
            .toPromise()
            .then((response: Response) => {
                var data: SyncConfiguration = <SyncConfiguration>response.json();
                return data;
            })
            .catch(this.handleError);
    }

    deleteById(id: string): Promise<boolean> {
        let url: string = `${this._configuration.ServerWithBaseUrl}syncconfigurations/${id}`;

        return this._http.delete(url)
            .toPromise()
            .then((response: Response) => {
                return response.status == 200;
            })
            .catch(this.handleError);
    }

    create(syncConfig: SyncConfiguration): Promise<boolean> {
        let url: string = `${this._configuration.ServerWithBaseUrl}syncconfigurations`;
        let body: string = JSON.stringify(syncConfig);
        let headers: Headers = new Headers({ "Content-Type": "application/json" });
        let options: RequestOptions = new RequestOptions({ headers: headers });
        
        return this._http.post(url, body, options)
            .toPromise()
            .then((response: Response) => {
                return response.status == 200;
            })
            .catch(this.handleError);
    }

    update(syncConfig: SyncConfiguration): Promise<boolean> {
        let url: string = `${this._configuration.ServerWithBaseUrl}syncconfigurations/${syncConfig.id}`;
        let body: string = JSON.stringify(syncConfig);
        let headers: Headers = new Headers({ "Content-Type": "application/json" });
        let options: RequestOptions = new RequestOptions({ headers: headers });

        return this._http.put(url, body, options)
            .toPromise()
            .then((response: Response) => {
                return response.status == 200;
            })
            .catch(this.handleError);
    }

    private handleError(error: Response): {} {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    };
}