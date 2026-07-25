import { type CellSymbol } from "./symbols";

export interface PuzzleNode {
    id: number;
    x: number;
    y: number;
    isStart: boolean;
    isEnd: boolean;
    hasDot: boolean;
}

export interface PuzzleEdge {
    id: number;
    nodeA: number;
    nodeB: number;
    hasDot: boolean;
}

export interface PuzzleCell {
    id: number;
    x: number;
    y: number;
    symbol: CellSymbol | null;
}

export interface PuzzleGrid {
    nodes: Record<string, PuzzleNode>;
    edges: Record<string, PuzzleEdge>;
    cells: Record<string, PuzzleCell>;

    backgroundColor: string;
    edgesColor: string;
    cellsColor: string;
    lineInputColor: string;
    lineInputSolvedColor: string;
}