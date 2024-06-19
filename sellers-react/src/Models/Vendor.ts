import { District } from "./District";

export type Vendor = {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    role: string;
    districts: District[];
}