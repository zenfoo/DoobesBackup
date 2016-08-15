import { Injectable } from "@angular/core";

@Injectable()
export class AppConfiguration {
    public Server: string = "http://localhost:5000/";
    public ApiUrl: string = "api/";
    public ServerWithApiUrl: string = this.Server + this.ApiUrl;
}