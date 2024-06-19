import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { Vendor } from "../Models/Vendor";

const api = "http://localhost:5078/api/vendors/";

export const vendorsGetAll = async () => {
    try {
        const data = await axios.get<Vendor[]>(api);
        return data;
    } catch (error) {
        handleError(error);
    }
};

export const vendorsGetByDistrictId = async (id: string) => {
    try {
        const data = await axios.get<Vendor>(api + "bydistrict/" + id);
        return data;
    } catch (error) {
        handleError(error);
    }
};

export const vendorsGetById = async (id: string) => {
    try {
        const data = await axios.get<Vendor>(api + id);
        return data;
    } catch (error) {
        handleError(error);
    }
};