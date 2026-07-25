import { useEffect, useState } from 'react';
import { puzzleApi } from './services/puzzleApi';
import { type PuzzleGrid } from './types';

export default function App() {
  const [grid, setGrid] = useState<PuzzleGrid | null>(null);
  const [error, setError] = useState<string>('');

  const testPuzzleId = '0675e46d-a39a-470c-8731-519f3996cefd';

  useEffect(() => {
    puzzleApi.getPuzzleById(testPuzzleId)
      .then(data => setGrid(data))
      .catch(err => setError(err.message));
  }, []);

  return (
    <div style={{ padding: '2rem', fontFamily: 'monospace' }}>
      <h1>Witness Puzzle Frontend Test</h1>
      {error && <p style={{ color: 'red' }}>Error: {error}</p>}
      {!grid && !error && <p>Loading puzzle from SQLite...</p>}
      {grid && (
        <div style={{ background: '#1a1a1a', color: '#00ff66', padding: '1rem', borderRadius: '8px' }}>
          <h3>Success! Loaded Grid:</h3>
          <pre>{JSON.stringify(grid, null, 2)}</pre>
        </div>
      )}
    </div>
  );
}

