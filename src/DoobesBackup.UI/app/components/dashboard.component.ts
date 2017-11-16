import { Component } from "@angular/core";

@Component({
    selector: "dashboard-panel",
    template: `
<div>
    <h2>Dashboard</h2>
    <div class="flex-row">
        <div class="flex-col">
            <p>Status</p>
        </div>
        <div class="flex-col">
            <p>&nbsp;</p>
        </div>
    </div>
    <div class="flex-row">
        <div class="flex-col">
            <p>&nbsp;</p>
        </div>
        <div class="flex-col">
            <p>&nbsp;</p>
        </div>
    </div>
</div>
`
})

export class DashboardComponent { }