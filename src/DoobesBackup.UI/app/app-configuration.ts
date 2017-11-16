import { Injectable } from "@angular/core";

@Injectable()
export class AppConfiguration {
    public Server: string = "http://localhost:5000/";
    public BaseUrl: string = "";
    public ServerWithBaseUrl: string = this.Server + this.BaseUrl;
}