import { useEffect, useState } from 'react';
import { puzzleApi } from './services/puzzleApi';
import { type PuzzleGrid } from './types';
import { PuzzleBoard } from './components/grid/PuzzleBoard';

export default function App() {
  const [grid, setGrid] = useState<PuzzleGrid | null>(null);
  const [error, setError] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(true);

  const testPuzzleId = '0675e46d-a39a-470c-8731-519f3996cefd';

  useEffect(() => {
    puzzleApi.getPuzzleById(testPuzzleId)
      .then(data => {
        setGrid(data);
        setLoading(false);
      })
      .catch(err => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  return (
    <div style={{ 
      minHeight: '100vh', 
      backgroundColor: '#a1a1a1', 
      color: '#ffffff', 
      display: 'flex', 
      flexDirection: 'column', 
      alignItems: 'center', 
      justifyContent: 'center',
      fontFamily: 'system-ui, sans-serif'
      }}>
      <h1 style={{ marginBottom: '2rem', letterSpacing: '2px', textTransform: 'uppercase', fontSize: '1.5rem', opacity: 0.8 }}>
        Witness Puzzle Engine
      </h1>
      {loading && <p>Loading puzzle...</p>}
      {error && <p style={{ color: 'red' }}>Error: {error}</p>}
      {!grid && !error && <p>Loading puzzle from SQLite...</p>}

      {grid && !loading && (
        <PuzzleBoard grid={grid} cellSize={80} />
      )}
    </div>
  );
}
