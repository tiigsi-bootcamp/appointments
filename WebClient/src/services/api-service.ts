import { Doctor, Specialty } from "../models/doctor";

const API_URL = 'https://localhost:7028/';

export class ApiService {
	static async getDoctors(page: number, size: number) {
		return await this.getData(`doctors?page=${page}&size=${size}`) as Doctor[];
	}

	static async getData<T>(url: string) {
		const response = await fetch(API_URL + url);
		const data = await response.json();

		return data as T;
	}
}
