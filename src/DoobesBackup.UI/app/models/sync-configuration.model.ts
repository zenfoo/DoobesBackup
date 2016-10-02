import { BackupDestination } from "./backup-destination.model";
import { BackupSource } from "./backup-source.model";

export class SyncConfiguration {
    id: number;
    name: string;
    intervalSeconds: number;
    //source: BackupSource;
    //destination: BackupDestination;
}