import { AlertType } from '../enums/alert-type.enum';

export interface IAlert {
    message?: string,
    type?: AlertType
}