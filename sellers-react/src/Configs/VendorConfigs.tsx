import { Badge } from "flowbite-react";
import { Vendor } from "../Models/Vendor";
import { toPascalCase } from "../Helpers/StringFormatting";
import { Link } from "react-router-dom";
import FormButton from "../Components/FormButton/FormButton";

export type VendorActions = {
    onRemove: (e: any) => void;
    onChangePrimary: (e: any) => void;
}

export const vendorsConfig = [
    {
        label: "Name",
        render: (vendor: Vendor) =>
            <Link className="whitespace-nowrap font-semibold hover:text-teal-500" to={`/vendors/${vendor.id}`}>{vendor.firstName + ' ' + vendor.lastName}</Link>
    },
    {
        label: "Email",
        render: (vendor: Vendor) => vendor.email
    },
    {
        label: "Phone Number",
        render: (vendor: Vendor) => vendor.phoneNumber
    },
    {
        label: "Role",
        render: (vendor: Vendor) =>
            <Badge className='inline' color={vendor.role === 'primary' ? "pink" : "indigo"}>{toPascalCase(vendor.role)}</Badge>
    },
    {
        label: "",
        render: (vendor: Vendor, actions: VendorActions) => {
            return (
                <div className="flex flex-wrap items-start gap-2">
                    {
                        vendor.role === 'primary'
                        ? <></>
                        : <>
                            <FormButton name="Remove" color="pink" id={vendor.id} onAction={actions?.onRemove} />
                            <FormButton name="To Primary" id={vendor.id} onAction={actions?.onChangePrimary} />
                        </>
                    }
                </div>
            )
        }
    }
];