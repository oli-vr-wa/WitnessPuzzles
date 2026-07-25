import React from 'react';
import { type PuzzleGrid } from '../../types';

interface PathLayerProps {
    grid: PuzzleGrid;
    path: number[];
    cellSize: number;
    padding: number;
    pathColor: string;
    currentMousePos?: { x: number, y: number } | null;
}

export const PathLayer: React.FC<PathLayerProps> = ({ grid, path, cellSize, padding, pathColor, currentMousePos }) => {
    if (path.length === 0) return null;

    const strokeWidth = cellSize * 0.18; // Adjust the stroke to match edge stroke width

    const lastNodeId = path[path.length - 1];
    const lastNode = grid.nodes[lastNodeId];
    const lastX = lastNode ? padding + lastNode.x * cellSize : 0;
    const lastY = lastNode ? padding + lastNode.y * cellSize : 0;    

    return (
        <g className="path-layer" style={{ pointerEvents: 'none' }}>
            {/* Draw line segments connecting sequential path nodes */}
            {path.map((nodeId, index) => {
                if (index === 0) return null; // Skip the first node since it has no previous node to connect to

                const prevNode = grid.nodes[path[index - 1]];
                const currentNode = grid.nodes[nodeId];

                if (!prevNode || !currentNode) return null; // Safety check

                const x1 = padding + prevNode.x * cellSize;
                const y1 = padding + prevNode.y * cellSize;
                const x2 = padding + currentNode.x * cellSize;
                const y2 = padding + currentNode.y * cellSize;

                return (
                    <line
                        key={`path-edge-${prevNode.id}-${currentNode.id}`}
                        x1={x1}
                        y1={y1}
                        x2={x2}
                        y2={y2}
                        stroke={pathColor}
                        strokeWidth={strokeWidth}
                        strokeLinecap="round"
                    />                    
                );
            })}

            {/* Draw a line from the last node to the current mouse position if the path is being drawn. Only X or Y may be updated to avoid drawing outside the edges. */}            
            {path.length > 0 && currentMousePos && lastNode && (() => {
                const dx = currentMousePos.x - lastX;
                const dy = currentMousePos.y - lastY;

                let targetX = lastX;
                let targetY = lastY;

                // Compare absolute distances to determine dominant axis
                if (Math.abs(dx) > Math.abs(dy)) {
                    const clampedX = Math.max(-cellSize, Math.min(cellSize, dx));
                    targetX = lastX + clampedX;
                } else {
                    const clampedY = Math.max(-cellSize, Math.min(cellSize, dy));
                    targetY = lastY + clampedY;
                }

                return (                
                    <line
                        x1={lastX}
                        y1={lastY}
                        x2={targetX}
                        y2={targetY}
                        stroke={pathColor}
                        strokeWidth={strokeWidth}
                        strokeLinecap="round"
                    />                
                );
            })()}

            {/* Draw joint circles so corners look round */}
            {path.map((nodeId) => {
                const node = grid.nodes[nodeId];
                if (!node) return null; // Safety check

                const cx = padding + node.x * cellSize;
                const cy = padding + node.y * cellSize;
                const r = node.isStart ? cellSize * 0.28 : node.isEnd ? cellSize * 0.18 : strokeWidth / 2;

                return <circle key={`path-node-${node.id}`} cx={cx} cy={cy} r={r} fill={pathColor} />;
            })}            
        </g> 
    );
};