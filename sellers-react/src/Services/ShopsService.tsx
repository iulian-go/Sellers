import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { Shop } from "../Models/Shop";

const api = "http://localhost:5078/api/shops/";

export const shopsGetByDistrictId = async (id: string) => {
    try {
        const data = await axios.get<Shop>(api + "bydistrict/" + id);
        return data;
    } catch (error) {
        handleError(error);
    }
};

export const shopsGetById = async (id: string) => {
    try {
        const data = await axios.get<Shop>(api + id);
        return data;
    } catch (error) {
        handleError(error);
    }
};