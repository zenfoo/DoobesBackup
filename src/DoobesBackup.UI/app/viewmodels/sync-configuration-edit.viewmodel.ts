import { SyncConfiguration } from "../models/sync-configuration.model";
import { DropDownOptionModel } from "../models/drop-down-option.model";

export class SyncConfigurationEditViewModel {
    syncConfig: SyncConfiguration = new SyncConfiguration();
    validIntervals: Array<DropDownOptionModel> = [
        new DropDownOptionModel("60", "1 min"),
        new DropDownOptionModel("300", "5 mins"),
        new DropDownOptionModel("1800", "30 mins"),
        new DropDownOptionModel("3600", "1 hour"),
        new DropDownOptionModel("7200", "2 hours"),
        new DropDownOptionModel("86400", "24 hours")
    ];
    backupSourceTypes: Array<DropDownOptionModel> = [
        new DropDownOptionModel("1", "Local Filesystem"),
        new DropDownOptionModel("2", "Network Share"),
        new DropDownOptionModel("3", "AWS S3")
    ];
    backupSourceType: number;
    isEditMode: boolean = false;
}