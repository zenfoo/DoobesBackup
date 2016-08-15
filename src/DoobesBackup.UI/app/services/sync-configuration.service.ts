import { Injectable } from "@angular/core";
import { HEROES } from "./mock-heroes";
import { Hero } from "../models/hero.model";

// import { Http, Response, Headers } from "@angular/http";
import "rxjs/add/operator/map";
import "rxjs/Rx";
// import { Observable } from "rxjs/Observable";
// import { SyncConfiguration } from "../models/sync-configuration.model";
import { AppConfiguration } from "../app-configuration";

@Injectable()
export class SyncConfigurationService {

    // private actionUrl: string;
    // private headers: Headers;

    constructor(/*private _http: Http, */private _configuration: AppConfiguration) {
        /*
        this.actionUrl = _configuration.ServerWithApiUrl + "syncconfigurations/";
        
        this.headers = new Headers();
        this.headers.append("Content-Type", "application/json");
        this.headers.append("Accept", "application/json");
        */
    }

    getAll(): Promise<Hero[]> {
        return new Promise<Hero[]>((resolve: (value: Hero[]) => void) => {
            setTimeout(() => resolve(HEROES), 2000)
        });
    }

    /*
    public GetAll = (): Observable<SyncConfiguration[]> => {
        return this._http.get(this.actionUrl)
            .map((response: Response) => <SyncConfiguration[]>response.json())
            .catch(this.handleError);
    };

    public GetSingle = (id: number): Observable<SyncConfiguration> => {
        return this._http.get(this.actionUrl + id)
            .map((response: Response) => <SyncConfiguration>response.json())
            .catch(this.handleError);
    };

    public Add = (itemName: string): Observable<SyncConfiguration> => {
        let toAdd:any = JSON.stringify({ ItemName: itemName });

        return this._http.post(this.actionUrl, toAdd, { headers: this.headers })
            .map((response: Response) => <SyncConfiguration>response.json())
            .catch(this.handleError);
    };

    public Update = (id: number, itemToUpdate: SyncConfiguration): Observable<SyncConfiguration> => {
        return this._http.put(this.actionUrl + id, JSON.stringify(itemToUpdate), { headers: this.headers })
            .map((response: Response) => <SyncConfiguration>response.json())
            .catch(this.handleError);
    };

    public Delete = (id: number): Observable<SyncConfiguration> => {
        return this._http.delete(this.actionUrl + id)
            .catch(this.handleError);
    };

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    };*/
}

interface ResolveCallback {
    (n: Hero[]): any;
}