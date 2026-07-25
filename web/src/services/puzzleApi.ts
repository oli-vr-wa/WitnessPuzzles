import { type PuzzleGrid } from '../types';

const BASE_URL = '/api/puzzles';

export interface CreatePuzzlePayload {
    name: string;
    grid: PuzzleGrid;
}

export const puzzleApi = {

    async getPuzzleById(id: string): Promise<PuzzleGrid> {
        const response = await fetch(`${BASE_URL}/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            if (response.status === 404) {
                throw new Error(`Puzzle with ID ${id} not found.`);
            }
            throw new Error(`Failed to fetch puzzle with ID ${id}: ${response.statusText}`);
        }

        const data: PuzzleGrid = await response.json();
        return data;
    },

    async createPuzzle(name: string, grid: PuzzleGrid): Promise<string> {
        const payload: CreatePuzzlePayload = { name, grid };

        const response = await fetch(BASE_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(payload),
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Failed to create puzzle: ${response.statusText} - ${errorText || response.statusText}`);
        }

        const data: { id: string } = await response.json();
        return data.id;
    },
}