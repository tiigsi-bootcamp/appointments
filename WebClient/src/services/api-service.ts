import { Doctor, Specialty } from "../models/doctor";

const API_URL = 'https://localhost:7028/';

export class ApiService {
	static async getDoctors(page: number, size: number): Promise<Doctor[]> {
		const url = `doctors?page=${page}&size=${size}`;
		return await this.getData(url);
	}

	static async getSpecialties() {
		const url = 'doctors/specialties';
		return await this.getData(url) as Specialty[];
	}

	private static async getData(url: string){
		const response = await fetch(API_URL + url);
		const data = await response.json();

		return data
	}
}
