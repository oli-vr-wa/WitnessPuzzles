import { useState, useMemo, useCallback } from 'react';
import { type PuzzleGrid } from '../types';

export function usePuzzlePath(grid: PuzzleGrid) {
    const [path, setPath] = useState<number[]>([]);
    const [isDrawing, setIsDrawing] = useState<boolean>(false);

    const adjacencyMap = useMemo(() => {
        const map = new Map<number, number[]>();

        Object.values(grid.edges).forEach(edge => {
            const idA = edge.nodeA.id;
            const idB = edge.nodeB.id;

            if (!map.has(idA)) map.set(idA, []);
            if (!map.has(idB)) map.set(idB, []);
            map.get(idA)!.push(idB);
            map.get(idB)!.push(idA);
        });

        return map;
    }, [grid.edges]);

    const startDrawing = useCallback((nodeId: number) => {
        const node = grid.nodes[nodeId];
        if (node && node.isStart) {
            setPath([nodeId]);
            setIsDrawing(true);
        }
    }, [grid.nodes]);

    const enterNode = useCallback((nodeId: number) => {
        if (!isDrawing || path.length === 0) return;

        const currentHead = path[path.length - 1];
        if (nodeId === currentHead) return; // Ignore if the same node is clicked

        // Check if the user is backtracking to the previous node
        if (path.length > 1 && nodeId === path[path.length - 2]) {
            setPath((prev) => prev.slice(0, -1)); // Remove the last node from the path
            return;
        }

        // Prevent self-intersection. Cannot revisit nodes already in the path unless backtracking.
        if (path.includes(nodeId)) return;

        // Verify an edge exists between the current head and the new node
        const neighbors = adjacencyMap.get(currentHead) || [];
        if (neighbors.includes(nodeId)) {
            setPath((prev) => [...prev, nodeId]);
        }
    }, [isDrawing, path, adjacencyMap]);

    const stopDrawing = useCallback(() => {
        if (!isDrawing) return;
        setIsDrawing(false);

        const finalNodeId = path[path.length - 1];
        const finalNode = grid.nodes[finalNodeId];

        // If the path didn't end on a valid end node, reset the path
        if (!finalNode || !finalNode.isEnd) {
            setPath([]);
        } else {
            console.log('Valid path completed:', path);
        }

    }, [isDrawing, path, grid.nodes]);

    return {
        path,
        isDrawing,
        startDrawing,
        enterNode,
        stopDrawing,
        resetPath: () => setPath([]),
    };
}