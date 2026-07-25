import React from 'react';
import { type PuzzleCell } from '../../types';
import { SymbolRenderer } from '../symbols/SymbolRenderer';

interface CellsLayerProps {
    cells: Record<string, PuzzleCell>;
    cellSize: number;
    padding: number;
    cellsColor: string;
}

export const CellsLayer: React.FC<CellsLayerProps> = ({ cells, cellSize, padding, cellsColor }) => {
    return (
        <g className="cells-layer">
            {Object.values(cells).map((cell) => {
                const x = padding + cell.x * cellSize;
                const y = padding + cell.y * cellSize;

                return (
                    <g key={cell.id} transform={`translate(${x}, ${y})`}>
                        <rect width={cellSize} height={cellSize} fill={cellsColor} />
                        {cell.symbol && <SymbolRenderer symbol={cell.symbol} cellSize={cellSize} />}
                    </g>
                );
            })}
        </g>
    );
};