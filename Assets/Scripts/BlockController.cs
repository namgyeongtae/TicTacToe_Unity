using System.Collections.Generic;
using UnityEngine;
using static Block;

public class BlockController : MonoBehaviour
{
    [SerializeField] private List<Block> blocks;

    public delegate void OnBlockClicked(int index);

    public OnBlockClicked onBlockClicked;

    // 블록 초기화
    public void InitBlocks()
    {
        for (int i = 0; i < 9; i++)
        {
            blocks[i].InitMarker(i, blockIndex => {
                onBlockClicked?.Invoke(blockIndex);
            });
        }
    }

    public void PlaceMarker(int blockIndex, MarkerType marker)
    {
        blocks[blockIndex].SetMarker(marker);
    }
}
