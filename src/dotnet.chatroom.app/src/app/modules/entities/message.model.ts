import { MessageType } from '../enums/messate-type.enum';

export interface IMessage<T> {
    id: string;
    emitter: string;
    emitterName: string;
    audience: string;
    type: MessageType;
    content: T;
    created: Date;
}