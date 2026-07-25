import React from 'react';
import { type PuzzleNode } from '../../types';

interface NodesLayerProps {
    nodes: Record<string, PuzzleNode>;
    cellSize: number;
    padding: number;
    edgesColor: string;
    onNodeMouseDown: (nodeId: number) => void;
    onNodeMouseEnter: (nodeId: number) => void;
}

export const NodesLayer: React.FC<NodesLayerProps> = ({ nodes, cellSize, padding, edgesColor, onNodeMouseDown, onNodeMouseEnter }) => {
    const baseRadius = cellSize * 0.06; // Mathes half the stroke width of edges (0.18 * cellSize)

    return (
        <g className="nodes-layer">
            {Object.values(nodes).map((node) => {
                const cx = padding + node.x * cellSize;
                const cy = padding + node.y * cellSize;

                return (
                    <g key={node.id}>
                        {/* Standard rounded corner joint */}
                        <circle cx={cx} cy={cy} r={baseRadius}  fill={edgesColor} />

                        {/* Start node: Large circle */}
                        {node.isStart && (
                            <circle cx={cx} cy={cy} r={cellSize * 0.28} fill={edgesColor} />
                        )}

                        {/* End node: Small rounded tip */}
                        {node.isEnd && (
                            <circle cx={cx} cy={cy} r={cellSize * 0.16} fill={edgesColor} stroke={edgesColor} strokeWidth={3} />
                        )}

                        {/* Hexagonal dot rule on node */}
                        {node.hasDot && (
                            <circle cx={cx} cy={cy} r={baseRadius * 1.4} fill="#1a1a1a" />
                        )}

                        {/* Invisible hit target for smooth mouse interactions */}
                        <circle
                            cx={cx}
                            cy={cy}
                            r={cellSize * 0.15} // Larger hit area for easier interaction
                            fill="transparent"
                            onMouseDown={() => onNodeMouseDown(node.id)}
                            onMouseEnter={() => onNodeMouseEnter(node.id)}
                        />
                    </g>
                );
            })}
        </g>
    )
};