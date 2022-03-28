import { Doctor, Specialty } from "../models/doctor";
import { TokenService } from "./token-service";

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

	static async login(data: { email: string, password: string }) {
		const url = API_URL + 'auth/login';
		const response = await fetch(url, {
			method: 'POST',
			body: JSON.stringify(data),
			headers: {
				'Content-Type': 'application/json'
			}
		});

		if (!response.ok) {
			return false;
		}

		const result = await response.json();
		const token = result.token;
		TokenService.save(token);
		
		return true;
	}

	static async loginWithProvider(data: { provider: string, token: string }) {
		const url = API_URL + 'auth/social-login';
		const response = await fetch(url, {
			method: 'POST',
			body: JSON.stringify(data),
			headers: {
				'Content-Type': 'application/json'
			}
		});

		if (!response.ok) {
			return false;
		}

		const result = await response.json();
		const token = result.token;
		TokenService.save(token);
		
		return true;
	}
}
