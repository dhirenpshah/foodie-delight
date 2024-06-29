import { FormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { NgModule } from '@angular/core';

@NgModule({
    imports:[
        FormsModule,
        CommonModule,
        ToastModule,
        ButtonModule,
        ConfirmDialogModule,
        TableModule,
        IconFieldModule,
        InputIconModule,
        InputTextModule
    ],
    exports: [
        FormsModule,
        CommonModule,
        ToastModule,
        ButtonModule,
        ConfirmDialogModule,
        TableModule,
        IconFieldModule,
        InputIconModule,
        InputTextModule
    ],
    providers: []
})

export class ImportsModule {}