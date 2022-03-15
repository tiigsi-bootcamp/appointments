import { Doctor } from "../models/doctor";

const API_URL = 'https://localhost:7028/';

export class ApiService {
	static async getDoctors(page:number, size:number): Promise<Doctor[]> {
		const url = `${API_URL}Doctors?page=${page}&size=${size}`;
		const response = await fetch(url);
		const data = await response.json();

		return data;
	}
}
