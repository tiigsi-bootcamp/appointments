import { Doctor, Specialty } from "../models/doctor";

const API_URL = 'https://localhost:7028/';

export class ApiService {
	static async getDoctors(page: number, size: number) {
		return await this.getData(`doctors?page=${page}&size=${size}`) as Doctor[];
	}

	static async getData<T>(url: string, ...args: number[]) {
		const response = await fetch(API_URL + url + '?page=' + args[0] + '&size=' + args[1]); 
		               //await fetch(`${API_URL}${url}?page=${args[0]}&size=${args[1]}`);
		const data = await response.json();

		return data as T;
	}
}
