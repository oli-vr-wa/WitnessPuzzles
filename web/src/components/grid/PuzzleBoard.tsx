import React from 'react';
import { usePuzzlePath } from '../../hooks/usePuzzlePath';
import { type PuzzleGrid } from '../../types';
import { CellsLayer } from './CellsLayer';
import { EdgesLayer } from './EdgesLayer';
import { NodesLayer } from './NodesLayer';
import { PathLayer } from './PathLayer';
import { validatePuzzleApi, type ValidationResult } from '../../services/validatePuzzleApi';

interface PuzzleBoardProps {
    puzzleId: string;
    grid: PuzzleGrid;
    cellSize: number;
}

export const PuzzleBoard: React.FC<PuzzleBoardProps> = ({ puzzleId, grid, cellSize = 80 }) => {
    const padding = cellSize * 0.8; 
    const { path, isDrawing, startDrawing, enterNode, stopDrawing } = usePuzzlePath(grid);

    const [mousePos, setMousePos] = React.useState<{ x: number, y: number } | null>(null);
    const svgRef = React.useRef<SVGSVGElement | null>(null);

    const [isValidating, setIsValidating] = React.useState(false);
    const [validationResult, setValidationResult] = React.useState<ValidationResult | null>(null);

    let pathColor = grid.lineInputColor;
    if (validationResult) {
        pathColor = validationResult.isValid ? '#8b922c' : grid.lineInputColor;
    }

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

    const handleStopDrawing = async () => {
        setMousePos(null);

        // Check if we ended on a valid node.
        const lastNodeId = path[path.length - 1];
        const lastNode = grid.nodes[lastNodeId];

        if (!lastNode || !lastNode.isEnd) {
            stopDrawing();
            setValidationResult(null);
            return;
        }
        
        stopDrawing();
        setIsValidating(true);
        setValidationResult(null);

        // Validate the solution
        try {
            const result = await validatePuzzleApi.validateSolution(puzzleId, path);
            setValidationResult(result);
        } catch (error) {
            console.error(error);
        } finally {
            setIsValidating(false);
        }
    };

    return (
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '1.5rem' }}>
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
                        <PathLayer grid={grid} path={path} cellSize={cellSize} padding={padding} pathColor={pathColor} currentMousePos={mousePos} />
                </svg>
            </div>

            {isValidating && <div style={{ color: 'white', fontSize: '1.2rem' }}>Validating...</div>}
            {validationResult && (
                <div style={{
                    padding: '1rem 1.5rem',
                    borderRadius: '8px',
                    backgroundColor: validationResult.isValid ? 'rgba(255, 215, 0, 0.15)' : 'rgba(255, 51, 51, 0.15)',
                    border: `1px solid ${pathColor}`,
                    color: pathColor,
                    fontFamily: 'monospace',
                    maxWidth: `${svgWidth}px`,
                    textAlign: 'center'
                }}>
                 <h3 style={{ margin: '0 0 0.5rem 0', textTransform: 'uppercase' }}>
            {validationResult.isValid ? '★ PUZZLE SOLVED! ★' : '✕ INCORRECT SOLUTION'}
                </h3>
                {!validationResult.isValid && validationResult.errors.length > 0 && (
                    <ul style={{ margin: 0, paddingLeft: '1.2rem', textAlign: 'left', fontSize: '0.9rem', color: '#ff9999' }}>
                    {validationResult.errors.map((err, i) => (
                        <li key={i}>{err}</li>
                    ))}
                    </ul>
                )}
                </div>
            )}            
        </div>
    );
};