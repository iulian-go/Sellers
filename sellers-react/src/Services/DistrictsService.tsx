import axios from "axios";
import { District } from "../Models/District";
import { handleError } from "../Helpers/ErrorHandler";

const api = "http://localhost:5078/api/districts/";

export const districtsGetAll = async () => {
    try {
        const data = await axios.get<District[]>(api);
        return data;
    } catch (error) {
        handleError(error);
    }
};

export const districtsGetById = async (id: string) => {
    try {
        const data = await axios.get<District>(api + id);
        return data;
    } catch (error) {
        handleError(error);
    }
};