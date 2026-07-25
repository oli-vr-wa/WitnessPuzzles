import React from 'react';
import { type CellSymbol } from '../../types';

interface SymbolRendererProps {
  symbol: CellSymbol;
  cellSize: number;
}

export const SymbolRenderer: React.FC<SymbolRendererProps> = ({ symbol, cellSize }) => {
    const center = cellSize / 2;

    switch (symbol.$type) {
        case 'square': {
            const size = cellSize * 0.45;
            const offset = (cellSize - size) / 2;
            return (
                <rect
                    x={offset}
                    y={offset}
                    width={size}
                    height={size}
                    rx={size * 0.2}
                    fill={symbol.colorHex}
                />
            );
        }
        case 'star': {
            const radius = cellSize * 0.22;
            return (
                <g transform={`translate(${center}, ${center})`}>
                    <polygon
                        points={`0,-${radius} ${radius * 0.35},-${radius * 0.35} ${radius},0 ${radius * 0.35},${radius * 0.35} 0,${radius} -${radius * 0.35},${radius * 0.35} -${radius},0 -${radius * 0.35},-${radius * 0.35}`}
                        fill={symbol.colorHex}
                    />
                    <polygon
                        points={`0,-${radius} ${radius * 0.35},-${radius * 0.35} ${radius},0 ${radius * 0.35},${radius * 0.35} 0,${radius} -${radius * 0.35},${radius * 0.35} -${radius},0 -${radius * 0.35},-${radius * 0.35}`}
                        fill={symbol.colorHex}
                        transform="rotate(45)"
                    />
                </g>
            );
        }
        case 'tetris': {
            const size = cellSize * 0.3;
            const offset = (cellSize - size) / 2;
            return (
                <g transform={`rotate(${symbol.rotation}, ${center}, ${center})`}>
                <rect
                    x={offset}
                    y={offset}
                    width={size}
                    height={size}
                    fill={symbol.isNegative ? 'none' : symbol.colorHex}
                    stroke={symbol.colorHex}
                    strokeWidth={symbol.isNegative ? 3 : 0}
                    strokeDasharray={symbol.isNegative ? '4 2' : 'none'}
                />
                {symbol.canRotate && (
                    <circle cx={center} cy={center} r={size * 0.15} fill="#1a1a1a" />
                )}
                </g>
            );
        }
        default:
            return null;
    }
};