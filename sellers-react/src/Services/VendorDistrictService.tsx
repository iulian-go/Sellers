import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { VendorDistrict } from "../Models/VendorDistrict";

const api = "http://localhost:5078/api/assignments/";

export const assignmentsAssignVendor = async (assignment: VendorDistrict) => {
    try {
        const data = await axios.post<VendorDistrict>(api, {
            vendorId: assignment.vendorId,
            districtId: assignment.districtId
        });
        return data;
    } catch (error) {
        handleError(error);
    }
}

export const assignmentsChangePrimary = async (assignment: VendorDistrict) => {
    try {
        const data = await axios.put<VendorDistrict>(api, {
            vendorId: assignment.vendorId,
            districtId: assignment.districtId
        });
        return data;
    } catch (error) {
        handleError(error);
    }
}

export const assignmentsRemoveVendor = async (assignment: VendorDistrict) => {
    try {
        const data = await axios.delete<VendorDistrict>(api, {
            data: {
                vendorId: assignment.vendorId,
                districtId: assignment.districtId
            }
        });
        return data;
    } catch (error) {
        handleError(error);
    }
}