import { ref } from 'vue';
import { useRouter } from 'vue-router'
import { TokenFields } from '../models/token-fields';

const router = useRouter();
export class TokenService {
	private static _tokenKey = '__token__';

	static isLoggedIn = ref(!!TokenService.get());
    

	static save(token: string) {
		localStorage.setItem(this._tokenKey, token);
		this.isLoggedIn.value = true;
	}
    
	static get() {
		return localStorage.getItem(this._tokenKey);
	}

	static delete() {
		localStorage.removeItem(this._tokenKey);
		
		this.isLoggedIn.value = false;
	}

	// Discriminated Unions.
	static decode(): TokenFields | null {
		const token = this.get();
		if (!token || token.length < 1) {
			return null;
		}

		const parts = token.split('.');
		if (parts.length != 3) {
			return null;
		}

		const input = parts[1]
			.replaceAll('-', '+')
			.replaceAll('_', '/');

		const decoded = atob(input);
		return JSON.parse(decoded) as TokenFields;
	}
}
