import { Component, Input } from "@angular/core";

@Component({
    moduleId: module.id,
    selector: "backup-source-config",
    templateUrl: "backup-source-config.component.html"
})

export class BackupSourceConfigComponent {
    _type: number;

    @Input()
    set backupSourceType(value: number) {
        this._type = value;
    }
    get backupSourceType(): number {
        return this._type;
    }
    test: number = 5;
}