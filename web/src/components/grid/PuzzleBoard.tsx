import React from 'react';
import { usePuzzlePath } from '../../hooks/usePuzzlePath';
import { type PuzzleGrid } from '../../types';
import { CellsLayer } from './CellsLayer';
import { EdgesLayer } from './EdgesLayer';
import { NodesLayer } from './NodesLayer';
import { PathLayer } from './PathLayer';

interface PuzzleBoardProps {
    grid: PuzzleGrid;
    cellSize: number;
}

export const PuzzleBoard: React.FC<PuzzleBoardProps> = ({ grid, cellSize = 80 }) => {
    const padding = cellSize * 0.8; 
    const { path, isDrawing, startDrawing, enterNode, stopDrawing } = usePuzzlePath(grid);

    const [mousePos, setMousePos] = React.useState<{ x: number, y: number } | null>(null);
    const svgRef = React.useRef<SVGSVGElement | null>(null);

    // Calculate grid dimensions
    const allNodes = Object.values(grid.nodes);
    const maxX = Math.max(0, ...allNodes.map(node => node.x));
    const maxY = Math.max(0, ...allNodes.map(node => node.y));
    const svgWidth = maxX * cellSize + padding * 2;
    const svgHeight = maxY * cellSize + padding * 2;

    // Convert browsermouse pixels into SVG coordinates
    const handleMouseMove = (e: React.MouseEvent) => { 
        if (!isDrawing || !svgRef.current) return;
        
        const rect = svgRef.current.getBoundingClientRect();
        const scaleX = svgWidth / rect.width;
        const scaleY = svgHeight / rect.height;

        // Translate coordinates to SVG space
        const x = (e.clientX - rect.left) * scaleX;
        const y = (e.clientY - rect.top) * scaleY;
        setMousePos({ x, y });
    };

    const handleStopDrawing = () => {
        setMousePos(null);
        stopDrawing();
    };

    return (
        <div
            onMouseMove={handleMouseMove}
            onMouseUp={handleStopDrawing}
            onMouseLeave={handleStopDrawing}
            style={{
                display: 'inline-block',
                padding: '1.5rem',
                backgroundColor: grid.cellsColor,
                borderRadius: '16px',
                boxShadow: '0 12px 32px rgba(0, 0, 0, 0.4)',
                border: `4px solid rgba(255, 255, 255, 0.08)`,
            }}
        >
            <svg 
                ref={svgRef}
                width={svgWidth}
                height={svgHeight}
                viewBox={`0 0 ${svgWidth} ${svgHeight}`}
                style={{ display: 'block', overflow: 'visible' }}
                >
                    {/* Layer 1: Cells and Symbols */}
                    <CellsLayer cells={grid.cells} cellSize={cellSize} padding={padding} cellsColor={grid.cellsColor} />
                    {/* Layer 2: Edges */}
                    <EdgesLayer edges={grid.edges} cellSize={cellSize} padding={padding} edgesColor={grid.edgesColor} />                    
                    {/* Layer 3: Nodes */}
                    <NodesLayer nodes={grid.nodes} cellSize={cellSize} padding={padding} edgesColor={grid.edgesColor} onNodeMouseDown={startDrawing} onNodeMouseEnter={enterNode} />
                    {/* Layer 4: Path */}
                    <PathLayer grid={grid} path={path} cellSize={cellSize} padding={padding} pathColor={grid.lineInputColor} currentMousePos={mousePos} />
            </svg>
        </div>
    );
};