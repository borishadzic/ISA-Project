import { ProfileModel } from "./profile-model";

export class InviteBindingModel {
    users: ProfileModel[];
    projectionIds: number[];
    rows: number[];
    columns: number[];
}