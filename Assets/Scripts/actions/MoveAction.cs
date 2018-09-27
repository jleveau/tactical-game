

public class MoveAction : Action {
	
   public MoveAction(AssemblyCSharp.Assets.Scripts.TileMapElements.Unit performer, UnityEngine.Vector3Int target, Board board) : base(performer, target, board)
	{
	}

	public override bool condition()
	{
		return this.board.getTileDistance(this.performer.tile_position, this.target) < this.performer.profile.movement_points;
	}

	public override void perform()
	{
		this.board.moveUnitTo(this.performer.gameObject, this.target);
	}
    

}
