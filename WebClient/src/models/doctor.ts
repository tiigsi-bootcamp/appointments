export interface Doctor {
	id: number;
	phone: string;
	specialty: string;
	picture: string;
	bio: string;
	certificate: string;
	ticketPrice: number;
	userId: number;
	user?: User;
}

export interface User {
	id: number;
	fullName: string;
	gender: string;
}

export interface Specialty {
	specialty: string;
	count: number;
}
