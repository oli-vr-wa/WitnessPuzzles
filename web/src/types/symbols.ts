export interface BaseSymbol {
    colorHex: string;
}

export interface SquareSymbol extends BaseSymbol {
    $type: 'square';
}

export interface StarSymbol extends BaseSymbol {
    $type: 'star';
}

export interface TetrisSymbol extends BaseSymbol {
    $type: 'tetris';
    shape: number;
    rotation: number;
    canRotate: boolean;
    isNegative: boolean;
}

export type CellSymbol = SquareSymbol | StarSymbol | TetrisSymbol;