using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal record CellVisualisation(char Content, string CssClass);

	internal record DefaultCellVisualisation() : CellVisualisation(' ', "f0-cell f0-cell-covered");
}
