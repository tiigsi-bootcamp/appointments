import { Doctor } from "../models/doctor";

const API_URL = 'https://localhost:7028/';

export class ApiService {
	static async getDoctors(): Promise<Doctor[]> {
		const url = API_URL + 'doctors?page=0&size=10';
		const response = await fetch(url);
		const data = await response.json();

		return data;
	}
}
