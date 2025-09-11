import { Component, inject } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ConfirmationData } from '../../interfaces';

@Component({
    selector: 'app-confirmation-dialog',
    standalone: true,
    imports: [
        MatDialogModule,
        MatButtonModule,
        MatIconModule
    ],
    templateUrl: './confirmation-dialog.html',
    styleUrls: ['./confirmation-dialog.scss']
})
export class ConfirmationDialog {
    private dialogRef = inject(MatDialogRef<ConfirmationDialog>);
    readonly data = inject<ConfirmationData>(MAT_DIALOG_DATA);

    onConfirm(): void {
        this.dialogRef.close(true);
    }

    onCancel(): void {
        this.dialogRef.close(false);
    }
}
