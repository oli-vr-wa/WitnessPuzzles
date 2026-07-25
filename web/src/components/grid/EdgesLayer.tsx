import React from 'react';
import { type PuzzleEdge } from '../../types';

interface EdgesLayerProps {
    edges: Record<string, PuzzleEdge>;
    cellSize: number;
    padding: number;
    edgesColor: string;
}

export const EdgesLayer: React.FC<EdgesLayerProps> = ({ edges, cellSize, padding, edgesColor }) => {
    const strokeWidth = cellSize * 0.18; // Adjust the stroke width based on cell size

    return (
        <g className="edges-layer">
            {Object.values(edges).map((edge) => {
                const x1 = padding + edge.nodeA.x * cellSize;
                const y1 = padding + edge.nodeA.y * cellSize;
                const x2 = padding + edge.nodeB.x * cellSize;
                const y2 = padding + edge.nodeB.y * cellSize;

                const midX = (x1 + x2) / 2;
                const midY = (y1 + y2) / 2;

                return (
                    <g key={edge.id}>
                        <line
                            x1={x1}
                            y1={y1}
                            x2={x2}
                            y2={y2}
                            stroke={edgesColor}
                            strokeWidth={strokeWidth}
                            strokeLinecap="round"
                        />
                        {edge.hasDot && (
                            <circle
                                cx={midX}
                                cy={midY}
                                r={strokeWidth * 0.3}
                                fill="#1a1a1a" // Dark background color for the dot
                            />
                        )}
                    </g>
                );
            })}
        </g>
    );
};