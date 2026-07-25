
const BASE_URL = '/api/puzzles';

export interface ValidationResult {
  isValid: boolean;
  errors: string[];
}

export const validatePuzzleApi = {
    async validateSolution(id: string, drawnNodeIds: number[]): Promise<ValidationResult> {
        const response = await fetch(`${BASE_URL}/${id}/validate`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify({ drawnNodeIds }),
        });

        if (!response.ok) {
            throw new Error(`Failed to validate puzzle solution: ${response.statusText}`);
        }
        
        return await response.json();
    }
};