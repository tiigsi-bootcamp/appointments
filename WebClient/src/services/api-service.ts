import { Doctor, Specialty } from "../models/doctor";

const API_URL = 'https://localhost:7028/';

export class ApiService {
	// TODO: Deprecate this function.
	static async getDoctors(page: number, size: number) {
		return await this.getData(`doctors?page=${page}&size=${size}`) as Doctor[];
	}

	static async getData<T>(url: string, queryParams?: {}) {
		const query = new URLSearchParams();
		if (queryParams) {
			const keys = Object.keys(queryParams);
			for (const key of keys) {
				// if (query.length > 1) {
				// 	query += '&';
				// }

				// query += key + '=' + (queryParams as any)[key];
				// console.log('Query', query);

				const parameterValue = (queryParams as any)[key];
				query.append(key, parameterValue);
			}
		}

		url += '?' + query.toString();
		
		const response = await fetch(API_URL + url);
		const data = await response.json();

		return data as T;
	}
}
